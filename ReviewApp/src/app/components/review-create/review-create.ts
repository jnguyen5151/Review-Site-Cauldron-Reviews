import { Component, signal, inject, PLATFORM_ID, NgZone } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { Field, form } from '@angular/forms/signals';

import { Review, initialData, reviewSchema } from '../../models/review';
import { ReviewService } from '../../services/review-service';

import { FormsModule } from '@angular/forms';
import { QuillModule } from 'ngx-quill'
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-review-create',
  imports: [Field, FormsModule, QuillModule],
  templateUrl: './review-create.html',
  styleUrl: './review-create.css',
})
export class ReviewCreate {

  reviewModel = signal<Review>(initialData);
  reviewForm = form(this.reviewModel, reviewSchema);

  private reviewService = inject(ReviewService);
  private ngZone = inject(NgZone);

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

  onEditorChange(content: string | null) {
    this.previewContent.set(
      this.sanitizer.bypassSecurityTrustHtml(content ?? '')
    );
  }

  submitReview() {
    const newReview = this.reviewModel();
    newReview.content = this.reviewContent;
    console.log(newReview);
    this.reviewService.createReview(newReview).subscribe();
  }
}
