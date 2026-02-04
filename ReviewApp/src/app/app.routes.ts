import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home';
import { ReviewDetails } from './components/review-details/review-details';
import { Register } from './components/register/register';
import { ReviewCreate } from './components/review-create/review-create';
import { VerifyEmail } from './components/verify-email/verify-email';
import { MyAccount } from './my-account/my-account';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home/1',
    pathMatch: 'full'
  },
  {
    path: 'home/:page',
    component: HomeComponent,
    title: 'Home Page'
  },
  {
    path: 'review/:reviewId',
    component: ReviewDetails,
    title: 'Review Page',
  },
  {
    path: 'register',
    component: Register,
    title: 'Register Page'
  },
  {
    path: 'create',
    component: ReviewCreate,
    title: 'Create a Review'
  },
  {
    path: 'verify-email/:userId/:token',
    component: VerifyEmail,
    title: 'Email Verification'
  },
  {
    path: 'myAccount',
    component: MyAccount,
    title: 'My Account'
  }
];
