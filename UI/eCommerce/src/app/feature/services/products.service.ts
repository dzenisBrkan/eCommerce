import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ProductOne } from '../models/products.model';
import { AddProducts } from '../models/add-products-request.model';
import { map } from 'rxjs/operators';
import { ProductResponse } from '../models/products-response.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiUrl = 'https://localhost:7221/api/product';
  // private apiUrlTest = 'https://dummyjson.com/products';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<ProductResponse> {
    return this.http.get<ProductResponse>(this.apiUrl);
  }

  // getProductById(id: number): Observable<Product> {
  //   return this.http.get<Product>(`${this.apiUrl}/Products/${id}`);
  // }

  // getProductsList(page: number, limit: number): Observable<ProductResponse> {
  //   return this.http.get<ProductResponse>(`${this.apiUrlTest}?skip=${(page - 1) * limit}&limit=${limit}`);
  // }
}
