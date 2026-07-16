import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

interface PurchaseItem {
  product_id: number;
  name: string;
  sku: string;
  quantity: number;
  cost_per_unit: number;
  subtotal: number;
}

@Component({
  selector: 'app-purchases',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css']
})
export class PurchasesComponent implements OnInit {
  purchases: any[] = [];
  products: any[] = [];
  
  // Active Purchase Form
  supplierName = '';
  purchaseItems: PurchaseItem[] = [];

  // Temporary item picker
  selectedProductId = 0;
  tempQuantity = 1;
  tempCost = 0;

  // View control
  activeTab: 'history' | 'new' = 'history';
  isLoading = false;
  isSaving = false;
  errorMessage = '';

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.loadPurchases();
    this.loadProducts();
  }

  loadPurchases() {
    this.isLoading = true;
    this.apiService.getPurchases().subscribe({
      next: (data) => {
        this.purchases = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error fetching purchases history:', err);
        this.isLoading = false;
      }
    });
  }

  loadProducts() {
    this.apiService.getProducts().subscribe({
      next: (data) => {
        this.products = data;
      },
      error: (err) => {
        console.error('Error loading products for purchases:', err);
      }
    });
  }

  onProductSelect() {
    const prod = this.products.find(p => p.id === Number(this.selectedProductId));
    if (prod) {
      this.tempCost = Number(prod.cost);
    }
  }

  addItemToPurchase() {
    if (!this.selectedProductId) {
      alert('Please select a product.');
      return;
    }
    if (this.tempQuantity <= 0) {
      alert('Quantity must be greater than 0.');
      return;
    }

    const prod = this.products.find(p => p.id === Number(this.selectedProductId));
    if (!prod) return;

    // Check if already in items list
    const existingIndex = this.purchaseItems.findIndex(item => item.product_id === prod.id);
    if (existingIndex !== -1) {
      const item = this.purchaseItems[existingIndex];
      item.quantity += this.tempQuantity;
      item.cost_per_unit = this.tempCost; // Update with latest selected cost
      item.subtotal = item.quantity * item.cost_per_unit;
    } else {
      this.purchaseItems.push({
        product_id: prod.id,
        name: prod.name,
        sku: prod.sku,
        quantity: this.tempQuantity,
        cost_per_unit: this.tempCost,
        subtotal: this.tempQuantity * this.tempCost
      });
    }

    // Reset picker inputs
    this.selectedProductId = 0;
    this.tempQuantity = 1;
    this.tempCost = 0;
  }

  removeItem(index: number) {
    this.purchaseItems.splice(index, 1);
  }

  get totalCost(): number {
    return this.purchaseItems.reduce((acc, item) => acc + item.subtotal, 0);
  }

  submitPurchase() {
    if (!this.supplierName.trim()) {
      alert('Please enter supplier name.');
      return;
    }
    if (this.purchaseItems.length === 0) {
      alert('Please add at least one item to purchase.');
      return;
    }

    this.isSaving = true;
    this.errorMessage = '';

    const payload = {
      supplier_name: this.supplierName,
      items: this.purchaseItems.map(item => ({
        product_id: item.product_id,
        quantity: item.quantity,
        cost_per_unit: item.cost_per_unit
      }))
    };

    this.apiService.createPurchase(payload).subscribe({
      next: () => {
        this.supplierName = '';
        this.purchaseItems = [];
        this.loadPurchases();
        this.loadProducts(); // Refresh costs/quantities
        this.isSaving = false;
        this.activeTab = 'history';
      },
      error: (err) => {
        console.error('Error saving purchase:', err);
        this.errorMessage = err.error?.error || 'Failed to submit restock purchase.';
        this.isSaving = false;
      }
    });
  }

  switchTab(tab: 'history' | 'new') {
    this.activeTab = tab;
    this.errorMessage = '';
  }
}
