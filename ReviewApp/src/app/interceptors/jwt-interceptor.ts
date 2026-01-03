import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError, switchMap } from 'rxjs';

import { AuthService } from '../services/auth-service';

let isRefreshing = false;

const PUBLIC_ENDPOINTS = [
  '/api/GameReview/getAll',
  '/api/GameReview/get',
  '/api/Account/Login',
  '/api/Account/Register',
  '/api/Account/ConfirmEmail',
  '/api/Account/Refresh'

]

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthService);
  const isPublic = PUBLIC_ENDPOINTS.some(url =>
    req.url.includes(url)
  );

  const authReq = isPublic ? req : req.clone({ withCredentials: true });

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401 && !isRefreshing && !isPublic) {

        isRefreshing = true;
        console.log('Interceptor is refreshing');

        return authService.refresh().pipe(
          switchMap(() => {
            isRefreshing = false;
            return next(req.clone({withCredentials:true}));
          }),
          catchError((refreshError: HttpErrorResponse) => {
            isRefreshing = false;
            authService.logout().subscribe();
            return throwError(() => refreshError);
          })
        );
      }

      return throwError(() => error);

    })
  )
}
