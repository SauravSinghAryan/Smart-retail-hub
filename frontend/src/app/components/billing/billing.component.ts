import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';

@Component({
selector:'app-billing',
standalone:true,
imports:[CommonModule,FormsModule],
templateUrl:'./billing.component.html',
styleUrl:'./billing.component.css'
})
export class BillingComponent implements OnInit{

invoices:any[]=[];
filteredInvoices:any[]=[];
selectedInvoice:any=null;

searchTerm='';
isLoading=false;
isDetailModalOpen=false;

constructor(private apiService:ApiService){}

ngOnInit():void{
this.loadInvoices();
}

loadInvoices():void{

this.isLoading=true;

this.apiService.getInvoices().subscribe({

next:(res:any)=>{

console.log("Invoice List",res);

this.invoices=Array.isArray(res)?res:[];

this.filteredInvoices=[...this.invoices];


this.isLoading=false;

},

error:(err:any)=>{

console.error(err);

this.isLoading=false;

}

});

}

onSearch():void{

const search=this.searchTerm.trim().toLowerCase();

this.filteredInvoices=this.invoices.filter(inv=>

(inv.invoiceNumber||'').toLowerCase().includes(search)||

(inv.customerName||'').toLowerCase().includes(search)||

(inv.customerPhone||'').toLowerCase().includes(search)

);

}

viewInvoiceDetails(id:number):void{

this.isLoading=true;

this.apiService.getInvoiceDetails(id).subscribe({

next:(res:any)=>{

console.log("Invoice Detail",res);

this.selectedInvoice=res;

this.isDetailModalOpen=true;

this.isLoading=false;

},

error:(err:any)=>{

console.error(err);

this.isLoading=false;

}

});

}

closeDetailModal():void{

this.selectedInvoice=null;

this.isDetailModalOpen=false;

}

printInvoice():void{

window.print();

}

}