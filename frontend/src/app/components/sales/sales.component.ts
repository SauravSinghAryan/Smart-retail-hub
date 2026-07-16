import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-sales',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './sales.component.html',
styleUrl: './sales.component.css'
})
export class SalesComponent implements OnInit {

  products:any[] = [];
  filteredProducts:any[] = [];
  cart:any[] = [];

  searchQuery = '';

  isLoading = false;
  isCheckingOut = false;

  customerName = 'Walk-in Customer';
  customerPhone = '';
  paymentMethod = 'Cash';

  discount = 0;
  taxRate = 18;

  errorMessage = '';

  subtotal = 0;
  taxAmount = 0;
  totalAmount = 0;

  isInvoiceModalOpen = false;

  createdInvoice:any = null;

  constructor(
    private api:ApiService
  ) {}

  ngOnInit():void {
    this.loadProducts();
  }

  loadProducts():void {

    this.isLoading = true;

    this.api.getProducts()
    .subscribe({

      next:(res)=>{

        this.products = res;

        this.filteredProducts = [...this.products];

        this.isLoading = false;

      },

      error:()=>{

        this.errorMessage = 'Unable to load products';

        this.isLoading = false;

      }

    });

  }

  onSearchChange():void {

    const search = this.searchQuery.toLowerCase();

    this.filteredProducts = this.products.filter(p=>
      p.name.toLowerCase().includes(search) ||
      p.sku.toLowerCase().includes(search)
    );

  }

  addToCart(product:any):void {

    const existing = this.cart.find(
      x=>x.product_id===product.id
    );

    if(existing){

      if(existing.quantity < product.quantity){

        existing.quantity++;

      }

    }
    else{

      this.cart.push({

        product_id:product.id,

        name:product.name,

        sku:product.sku,

        price_per_unit:product.price,

        quantity:1,

        subtotal:product.price

      });

    }

    this.calculateBill();

  }

  removeFromCart(item:any):void {

    this.cart = this.cart.filter(
      x=>x!==item
    );

    this.calculateBill();

  }

  clearCart():void {

    this.cart=[];

    this.calculateBill();

  }
  updateQuantity(item:any, change:number):void {

    const product = this.products.find(
      p=>p.id===item.product_id
    );

    if(!product){
      return;
    }

    let newQty = item.quantity + change;

    if(newQty < 1){
      newQty = 1;
    }

    if(newQty > product.quantity){
      newQty = product.quantity;
    }

    item.quantity = newQty;

    this.updateItemSubtotal(item);

    this.calculateBill();

  }

  onQuantityInput(item:any):void {

    const product = this.products.find(
      p=>p.id===item.product_id
    );

    if(!product){
      return;
    }

    if(item.quantity < 1){
      item.quantity = 1;
    }

    if(item.quantity > product.quantity){
      item.quantity = product.quantity;
    }

    this.updateItemSubtotal(item);

    this.calculateBill();

  }

  updateItemSubtotal(item:any):void {

    item.subtotal =
      item.quantity *
      item.price_per_unit;

  }

  calculateBill():void {

    this.subtotal = this.cart.reduce(
      (sum,item)=>
      sum + item.subtotal,
      0
    );

    let taxableAmount =
      this.subtotal - this.discount;

    if(taxableAmount < 0){
      taxableAmount = 0;
    }

    this.taxAmount =
      taxableAmount *
      this.taxRate /
      100;

    this.totalAmount =
      taxableAmount +
      this.taxAmount;

  }

  checkout(): void {

  if (this.cart.length === 0) {
    this.errorMessage = 'Cart is empty';
    return;
  }

  this.isCheckingOut = true;
  this.errorMessage = '';

  const saleData = {

    customer_name: this.customerName || 'Walk-in Customer',

    customer_phone: this.customerPhone,

    payment_method: this.paymentMethod,

    discount: this.discount,

    tax_rate: this.taxRate,

    subtotal: this.subtotal,

    tax_amount: this.taxAmount,

    total_amount: this.totalAmount,

    items: this.cart.map(item => ({

      product_id: item.product_id,

      quantity: item.quantity,

      price_per_unit: item.price_per_unit,

      subtotal: item.subtotal

    }))
  };

  this.api.createSale(saleData).subscribe({

    next: (res: any) => {

      // Fetch complete invoice
      this.api.getInvoiceDetails(res.saleId).subscribe({

        next: (invoice: any) => {

          this.createdInvoice = invoice;

          this.isInvoiceModalOpen = true;

          this.isCheckingOut = false;

          this.clearCart();

          this.loadProducts();

        },

        error: () => {

          this.errorMessage = 'Unable to load invoice.';

          this.isCheckingOut = false;

        }

      });

    },

    error: (err) => {

      this.errorMessage =
        err?.error?.message || 'Sale failed';

      this.isCheckingOut = false;

    }

  });

}
    closeInvoiceModal():void {

    this.isInvoiceModalOpen = false;

    this.createdInvoice = null;

  }


  printInvoice():void {

    window.print();

  }


  resetSale():void {

    this.cart = [];

    this.customerName = 'Walk-in Customer';

    this.customerPhone = '';

    this.paymentMethod = 'Cash';

    this.discount = 0;

    this.taxRate = 18;

    this.errorMessage = '';

    this.calculateBill();

  }


  refreshProducts():void {

    this.loadProducts();

  }

}