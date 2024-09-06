import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ProductDto, ProductsService } from '../../../core/services/products.service';

@Component({
  selector: 'app-edit-product-modal',
  templateUrl: './edit-product-modal.component.html',
  styleUrls: ['./edit-product-modal.component.css']
})
export class EditProductModalComponent implements OnInit {
  editProductForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private productsService: ProductsService,
    private dialogRef: MatDialogRef<EditProductModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ProductDto
  ) {}

  ngOnInit(): void {
    this.editProductForm = this.fb.group({
      id: [this.data.id],
      title: [this.data.title, Validators.required],
      category: [this.data.category, Validators.required],
      price: [this.data.price, Validators.required],
      quantity: [this.data.quantity, Validators.required],
      description: [this.data.description, Validators.maxLength(256)]
    });
  }

  onSave(): void {
    if (this.editProductForm.valid) {
      const updatedProduct = this.editProductForm.value;
      this.productsService.editProduct(updatedProduct).subscribe(() => {
        this.dialogRef.close(updatedProduct);
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
