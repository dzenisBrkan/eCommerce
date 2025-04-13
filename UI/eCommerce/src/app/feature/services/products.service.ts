import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Product, ProductResponse } from '../models/products-response.model';
import { ProductDetails } from '../models/products-details.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7221/api/product';

  constructor(private http: HttpClient) { }

  getProducts(page: number, productsPerPage: number, sortBy: string, orderBy: string): Observable<ProductResponse> {
    // return this.http.get<ProductResponse>(this.apiUrl);
    const skip = (page - 1) * productsPerPage;
    const url = `${this.apiUrl}?productsPerPage=${productsPerPage}&page=${page}&sortBy=${sortBy}&orderBy=${orderBy}`;

    return this.http.get<ProductResponse>(url);
  }

  searchProducts(query: string): Observable<ProductResponse> {
    const url = `${this.apiUrl}/search?q=${query}`;
    return this.http.get<ProductResponse>(url);
  }  

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }
}