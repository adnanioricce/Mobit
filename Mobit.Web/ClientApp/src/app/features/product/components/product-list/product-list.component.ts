import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ProductsService, ProductDto } from '../../../../core/services/products.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EditProductModalComponent } from '../../../../share/components/edit-product-modal/edit-product-modal.component';
import { AddProductModalComponent } from '../../../../share/components/add-product-modal/add-product-modal.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'title', 'category', 'price', 'actions'];
  dataSource = new MatTableDataSource<ProductDto>();
  totalProducts = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private productsService: ProductsService
    ,private dialog: MatDialog
    ,private router: Router
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(page: number = 0, pageSize: number = 10): void {
    this.productsService.getProducts(page + 1, pageSize).subscribe((products) => {
      this.dataSource.data = products;
      this.totalProducts = products.length; // Replace with total count from API
    });
  }

  onPageChange(event: PageEvent): void {
    this.loadProducts(event.pageIndex, event.pageSize);
  }

  viewDetails(id: number): void {
    this.router.navigate(['/products', id]);
  }
  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddProductModalComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe((newProduct) => {
      if (newProduct) {
        this.loadProducts();
      }
    });
  }
  openEditDialog(product: ProductDto): void {
    const dialogRef = this.dialog.open(EditProductModalComponent, {
      width: '400px',
      data: { ...product }
    });

    dialogRef.afterClosed().subscribe((updatedProduct) => {
      if (updatedProduct) {
        this.loadProducts();
      }
    });
  }
  onUploadSuccess(): void {
    this.loadProducts();    
  }
  deleteProduct(id: number): void {
    this.productsService.deleteProduct(id).subscribe(() => {
      this.loadProducts(this.paginator.pageIndex, this.paginator.pageSize);
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
