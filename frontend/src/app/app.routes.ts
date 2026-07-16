import { Routes } from '@angular/router';

export const routes: Routes = [
 {
    path: '',
    redirectTo: 'login', // 1. Change the landing page redirect from 'dashboard' to 'login'
    pathMatch: 'full'
  },
  {
    path: 'login', // 2. Add the route for your Login Component
    loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'dashboard',
    loadComponent: () => import('./components/dashboard/dashboard.component').then(m => m.DashboardComponent)
  },
  {
    path: 'products',
    loadComponent: () => import('./components/products/products.component').then(m => m.ProductsComponent)
  },
  {
    path: 'sales',
    loadComponent: () => import('./components/sales/sales.component').then(m => m.SalesComponent)
  },
  {
    path: 'purchases',
    loadComponent: () => import('./components/purchases/purchases.component').then(m => m.PurchasesComponent)
  },
  {
    path: 'billing',
    loadComponent: () => import('./components/billing/billing.component').then(m => m.BillingComponent)
  }
];
