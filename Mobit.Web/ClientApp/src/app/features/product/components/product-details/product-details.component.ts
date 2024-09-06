import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductsService, ProductDto } from '../../../../core/services/products.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product!: ProductDto;

  constructor(private route: ActivatedRoute, private productsService: ProductsService) {}

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.loadProductDetails(id);
  }

  loadProductDetails(id: number): void {
    this.productsService.getProductById(id).subscribe((product) => {
      this.product = product;
    });
  }
}
