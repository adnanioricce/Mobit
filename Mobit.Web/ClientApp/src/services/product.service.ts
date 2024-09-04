import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
// Define the ProductDto interface based on the C# ProductDto class.
export interface ProductDto {
  id: number;
  quantity: number;
  price: number;
  title: string;
  category: string;
  description: string;
}

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private apiUrl = `${environment.API_BASE_URL}/products`

  constructor(private http: HttpClient) {}

  // GET: api/products/{id}
  getProductById(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.apiUrl}/${id}`);
  }

  // GET: api/products?page=1&pageCount=100
  getProducts(page: number = 1, pageCount: number = 100): Observable<ProductDto[]> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageCount', pageCount.toString());
    return this.http.get<ProductDto[]>(this.apiUrl, { params });
  }

  // POST: api/products
  createProduct(product: ProductDto): Observable<ProductDto> {
    return this.http.post<ProductDto>(this.apiUrl, product, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  // PUT: api/products
  editProduct(product: ProductDto): Observable<void> {
    return this.http.put<void>(this.apiUrl, product, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  // DELETE: api/products/{id}
  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
