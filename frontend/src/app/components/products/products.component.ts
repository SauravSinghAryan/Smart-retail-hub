import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products: any[] = [];
  categories: string[] = [
  'Electronics',
  'Hardware',
  'Grocery',
  'Fashion',
  'Home & Kitchen',
  'Medical',
  'Stationery',
  'Sports',
  'Automotive',
  'Others'
];
  
  // Search / Filters
  searchTerm = '';
  selectedCategory = '';
  showLowStockOnly = false;
  isLoading = false;

  // Modals state
  isAddModalOpen = false;
  isEditModalOpen = false;
  errorMessage = '';

  // Form Fields
  productForm = {
    id: 0,
    name: '',
    sku: '',
    category: 'Hardware',
    price: 0,
    cost: 0,
    quantity: 0,
    min_stock_level: 5,
    supplier: ''
  };

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.isLoading = true;
    this.apiService.getProducts({
      category: this.selectedCategory,
      search: this.searchTerm,
      lowStock: this.showLowStockOnly
    }).subscribe({
      next: (data) => {
        this.products = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching products:', err);
        this.isLoading = false;
      }
    });
  }

  // Filter actions
  onSearch() {
    this.loadProducts();
  }

  onFilterCategory(cat: string) {
    this.selectedCategory = this.selectedCategory === cat ? '' : cat;
    this.loadProducts();
  }

  toggleLowStock() {
    this.showLowStockOnly = !this.showLowStockOnly;
    this.loadProducts();
  }

  // Modal Open/Close helpers
  openAddModal() {
    this.errorMessage = '';
    this.productForm = {
      id: 0,
      name: '',
      sku: this.generateSKUPrefix(),
      category: 'Hardware',
      price: 0,
      cost: 0,
      quantity: 0,
      min_stock_level: 5,
      supplier: ''
    };
    this.isAddModalOpen = true;
  }

  closeAddModal() {
    this.isAddModalOpen = false;
  }

  openEditModal(product: any) {
    this.errorMessage = '';
    this.productForm = { ...product }; // Shallow copy
    this.isEditModalOpen = true;
  }

  closeEditModal() {
    this.isEditModalOpen = false;
  }

  onCategoryChange() {
    // Regenerate SKU preview helper when category shifts
    if (!this.productForm.id) {
      this.productForm.sku = this.generateSKUPrefix();
    }
  }

  generateSKUPrefix(): string {
    const categoryMapping: { [key: string]: string } = {
      'Power Tools': 'POW-',
      'Hand Tools': 'HND-',
      'Electrical': 'ELC-',
      'Plumbing': 'PLM-',
      'Paints & Finishes': 'PNT-',
      'Fasteners': 'FST-',
      'Safety Equipment': 'SAF-'
    };
    const prefix = categoryMapping[this.productForm.category] || 'GEN-';
    const rand = Math.floor(100 + Math.random() * 900);
    return `${prefix}${rand}`;
  }

  // Form Submit CRUD Actions
  submitAddProduct() {
    this.errorMessage = '';
    this.apiService.addProduct(this.productForm).subscribe({
      next: (newProduct) => {
        this.products.unshift(newProduct);
        this.closeAddModal();
        this.loadProducts(); // Reload to apply filter/sort
      },
      error: (err) => {
        console.error('Error adding product:', err);
        this.errorMessage = err.error?.error || 'Failed to create product.';
      }
    });
  }

  submitEditProduct() {
    this.errorMessage = '';
    this.apiService.updateProduct(this.productForm.id, this.productForm).subscribe({
      next: (updated) => {
        const index = this.products.findIndex(p => p.id === updated.id);
        if (index !== -1) {
          this.products[index] = updated;
        }
        this.closeEditModal();
        this.loadProducts();
      },
      error: (err) => {
        console.error('Error editing product:', err);
        this.errorMessage = err.error?.error || 'Failed to update product.';
      }
    });
  }

  deleteProduct(id: number, name: string) {
    if (confirm(`Are you sure you want to delete '${name}'?`)) {
      this.apiService.deleteProduct(id).subscribe({
        next: () => {
          this.products = this.products.filter(p => p.id !== id);
        },
        error: (err) => {
          console.error('Error deleting product:', err);
          alert('Could not delete product. It may be referenced in invoice histories.');
        }
      });
    }
  }
}
