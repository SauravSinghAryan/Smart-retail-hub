import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = 'https://localhost:7168/api';

  constructor(private http: HttpClient) { }

  private getHeaders() {
    const token = localStorage.getItem('auth_token');

    return {
      headers: {
        Authorization: token ? `Bearer ${token}` : ''
      }
    };
  }


  // ================= Dashboard =================

  getDashboardStats(): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl}/dashboard/stats`,
      this.getHeaders()
    );
  }


  // ================= Products =================

  getProducts(filters?: {
    category?: string;
    search?: string;
    lowStock?: boolean;
  }): Observable<any[]> {

    let params = new HttpParams();

    if (filters) {

      if (filters.category) {
        params = params.set('category', filters.category);
      }

      if (filters.search) {
        params = params.set('search', filters.search);
      }

      if (filters.lowStock) {
        params = params.set('lowStock', 'true');
      }

    }

    return this.http.get<any[]>(
      `${this.baseUrl}/products`,
      {
        params,
        ...this.getHeaders()
      }
    );
  }


  addProduct(product: any): Observable<any> {
    return this.http.post<any>(
      `${this.baseUrl}/products`,
      product,
      this.getHeaders()
    );
  }


  updateProduct(id: number, product: any): Observable<any> {
    return this.http.put<any>(
      `${this.baseUrl}/products/${id}`,
      product,
      this.getHeaders()
    );
  }


  deleteProduct(id: number): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl}/products/${id}`,
      this.getHeaders()
    );
  }



  // ================= Sales =================

  getSales(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.baseUrl}/sales`,
      this.getHeaders()
    );
  }


  createSale(saleData: any): Observable<any> {
    return this.http.post<any>(
      `${this.baseUrl}/sales`,
      saleData,
      this.getHeaders()
    );
  }



  // ================= Purchases =================

  getPurchases(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.baseUrl}/purchases`,
      this.getHeaders()
    );
  }


  createPurchase(purchaseData: any): Observable<any> {
    return this.http.post<any>(
      `${this.baseUrl}/purchases`,
      purchaseData,
      this.getHeaders()
    );
  }



  // ================= Billing =================

  // Get all invoice records
  getInvoices(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.baseUrl}/billing/invoices`,
      this.getHeaders()
    );
  }


  // Get single invoice details
  getInvoiceDetails(id: number): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl}/billing/invoices/${id}`,
      this.getHeaders()
    );
  }

}