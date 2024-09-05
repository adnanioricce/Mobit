import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ProductsService, ProductDto } from './products.service';

@Component({
  selector: 'app-add-product-modal',
  templateUrl: './add-product-modal.component.html',
  styleUrls: ['./add-product-modal.component.css']
})
export class AddProductModalComponent implements OnInit {
  addProductForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private productsService: ProductsService,
    private dialogRef: MatDialogRef<AddProductModalComponent>
  ) {}

  ngOnInit(): void {
    this.addProductForm = this.fb.group({
      title: ['', Validators.required],
      category: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0.01)]],
      quantity: [0, [Validators.required, Validators.min(1)]],
      description: ['', Validators.maxLength(256)]
    });
  }

  onCreate(): void {
    if (this.addProductForm.valid) {
      const newProduct: ProductDto = this.addProductForm.value;
      this.productsService.createProduct(newProduct).subscribe(() => {
        this.dialogRef.close(newProduct);
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
