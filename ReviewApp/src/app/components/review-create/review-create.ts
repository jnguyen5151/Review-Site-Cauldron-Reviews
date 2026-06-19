import { Component, signal, inject, PLATFORM_ID, NgZone, ViewChild, ElementRef, HostListener, ChangeDetectionStrategy } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { FormField, form } from '@angular/forms/signals';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { schema, required, min, max, disabled } from '@angular/forms/signals';
import { QuillModule } from 'ngx-quill'

import { toObservable } from '@angular/core/rxjs-interop';
import { debounceTime, filter, switchMap, tap } from 'rxjs/operators';

import { Review, initialData} from '../../models/review';
import { ReviewService } from '../../services/review-service';
import { SearchService } from '../../services/search-service';
import { CardModel } from '../../models/game-search';



@Component({
  selector: 'app-review-create',
  imports: [FormField, FormsModule, QuillModule],
  templateUrl: './review-create.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './review-create.css',
})
export class ReviewCreate {

  reviewModel = signal<Review>(initialData);
  reviewForm = form(this.reviewModel, (root) => {
    required(root.gameName, { message: 'Please select a Game' });
    disabled(root.gameName, { when: ({ valueOf }) => valueOf(root.steamAppId) !== 0 });
    required(root.rating, { message: 'Rating is Required' });
    min(root.rating, 0, { message: 'Rating must be from 0 - 100' });
    max(root.rating, 100, { message: 'Rating must be from 0 - 100' });
    required(root.content, { message: 'Review Content is Required' });
    required(root.title, { message: 'Title is Required' });
  });

  private reviewService = inject(ReviewService);
  private searchService = inject(SearchService);
  private ngZone = inject(NgZone);
  private router = inject(Router);

  reviewContent = '';
  previewContent = signal<SafeHtml>('');
  isBrowser = false;
  public quillConfig: any = null;
  private static quillIntialized = false;
  private platformId = inject(PLATFORM_ID);

  editorReady = signal(false);


  constructor(private sanitizer: DomSanitizer) {
    this.isBrowser = isPlatformBrowser(this.platformId);

    if (this.isBrowser) {
      Promise.all([
        import('ngx-quill'),
        import('quill'),
        import('@botom/quill-resize-module')
      ]).then(([ngxQuill, quillModule, resizeModule]) => {

        if (!ReviewCreate.quillIntialized) {
          const Quill = quillModule.default;

          const BaseImageFormat = Quill.import('formats/image') as any;

          class ImageFormat extends BaseImageFormat {
            static formats(domNode: HTMLElement) {
              const formats = super.formats(domNode);
              if (domNode.hasAttribute('style')) {
                formats['style'] = domNode.getAttribute('style');
              }
              if (domNode.hasAttribute('width')) {
                formats['width'] = domNode.getAttribute('width');
              }
              if (domNode.hasAttribute('height')) {
                formats['height'] = domNode.getAttribute('height');
              }
              return formats;
            }

            format(name: string, value: any) {
              const node = (this as any)['domNode'] as HTMLElement;
              if (name === 'style' || name === 'width' || name === 'height') {
                if (value) {
                  node.setAttribute(name, value);
                } else {
                  node.removeAttribute(name);
                }
              } else {
                super.format(name, value);
              }
            }
          }

          Quill.register(ImageFormat, true);
          Quill.register('modules/resize', resizeModule.default);
          ReviewCreate.quillIntialized = true;
        }
        
        
        this.editorReady.set(true);

        this.quillConfig = {
          modules: {
            resize: {
              onChange: (quill: any) => {
                quill.update();
              }
            },
            toolbar: [
              ['clean'],
              ['bold', 'italic', 'underline', 'strike'],
              [{ color: [] }],
              [{ align: [] }],
              [{ header: 1 }, { header: 2 }],
              [{ size: ['small', false, 'large', 'huge'] }],
              [{ list: 'ordered' }, { list: 'bullet' }],
              ['link', 'image', 'video']
            ]
          },
          formats: [ 'bold', 'italic', 'underline',
                     'strike', 'color', 'align',
                     'header', 'size', 'list',
                     'link', 'image', 'video']
        };
      });
    }
  }

  ngOnInit() {
    this.searchResults$.subscribe((results: CardModel[]) => this.searchResults.set(results));
  }

  onEditorChange(content: string | null) {
    const cleaned = (content ?? '').replace(/&nbsp;/g, ' ');
    this.previewContent.set(
      this.sanitizer.bypassSecurityTrustHtml(cleaned ?? '')
    );
  }

  submitReview() {
    const newReview = this.reviewModel();
    newReview.content = this.reviewContent.replace(/&nbsp;/g, ' ');
    this.reviewService.createReview(newReview).subscribe(() => {
      this.router.navigate(['/']);
    });
  }

  searchString = signal<string>('');
  private searchString$ = toObservable(this.searchString);

  private searchResults$ = this.searchString$.pipe(
    filter((search: string) => search.length > 0),
    debounceTime(300),
    switchMap((search: string) => this.searchService.gameSearch({ search, appCount: 100}))
  );

  searchResults = signal<CardModel[]>([]);

  selectedGame: CardModel | null = null;

  @ViewChild('searchWrapper') searchWrapper!: ElementRef;

  @HostListener('document:click', ['$event.target'])
  onClick(target: EventTarget | null) {
    if (this.searchResults().length === 0) return;
    if (!this.searchWrapper.nativeElement.contains(target)) this.searchResults.set([]);
  }

  selectGame(game: CardModel) {
    this.selectedGame = game;
    this.reviewForm.gameName().value.set(game.name);
    this.reviewForm.steamAppId().value.set(game.appId);
    console.log(this.reviewForm.steamAppId().value());
  }

}
