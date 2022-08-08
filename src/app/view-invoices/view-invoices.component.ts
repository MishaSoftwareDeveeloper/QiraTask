import { Component, OnInit } from '@angular/core';
import { Invoice } from '../Models/invoice.model';
import { BackendService } from '../Services/backend.service';
import { ViewInvoicesService } from './view-invoices.service';
import {MessageService} from 'primeng/api';
import { Statuses } from '../Models/statuses.model';
import { PaymentMethods } from '../Models/payment-methods.model';
@Component({
  selector: 'app-view-invoices',
  templateUrl: './view-invoices.component.html',
  styleUrls: ['./view-invoices.component.scss']
})
export class ViewInvoicesComponent implements OnInit {

  public invoices:Invoice[];
  private clonedInvoice:{[id: string]:Invoice} = {};
  private page:number = 0;
  public createdDateInput:string = '';
  public statusInput:string = '';
  public paymentInput:string = ''
  private filter:string='';
  private filterBy:string=''
  private sortOrder:string='';
  totalRecords: number = 0;
  public statuses:any;
  public payments:any;
  public showDialog:boolean = false;
  public invoiceToSave:Invoice;
  public submitted:boolean=false;

  constructor(private api: BackendService, public viewInvSrv:ViewInvoicesService, private msgServ:MessageService) { }

  ngOnInit(): void {
    this.api.get().subscribe((res:any)=>{
      if(res){
        this.totalRecords = res.length

        let filteredStatuses = res.filter((item:any,index:number)=>{
          let adx = res.findIndex((x:any) => x.status === item.status);
          return adx == index;
        })
        this.statuses = filteredStatuses.map((inv:any)=>({
          label:this.viewInvSrv.getStatusName(inv.status),
          value:inv.status
        }));

        let filteredPayments= res.filter((item:any,index:number)=>{
          let adx = res.findIndex((x:any) => x.payment === item.payment);
          return adx == index;
        })

        this.payments = filteredPayments.map((inv:any)=>({
          label:this.viewInvSrv.getPaymentName(inv.payment),
          value:inv.payment
        }));

      }
    },(er)=>{
      console.error(er.error);
    });

   this.getInvoices(this.filter,this.filterBy,this.sortOrder,0);
  }

  private getInvoices(filtered:string,filterdBy:string, sortOrder:string, page:number){
    this.api.getFiltered(filtered,filterdBy,sortOrder,page).subscribe((res)=>{
      if(res){
        this.invoices = res as Invoice[];
      }
    },(er)=>{
      console.error(er.error);
    });
  }

  onPage(ev:any){
    this.page = ev.page;
    this.getInvoices(this.filter,this.filterBy,this.sortOrder,ev.page);
  }
  onSort(ev:any){
    this.sortOrder = ev.order > 0 ? ev.field : ''; 
    this.page = 0;
    this.getInvoices(this.filter,this.filterBy,this.sortOrder,this.page);
  }

  onFilter(filterBy:string,ev:any){
    if(ev.target.value.length > 2){
      this.filter = ev.target.value;
      this.filterBy = filterBy;
      this.page=0;
      switch(filterBy){
        case 'Status':
          this.createdDateInput = '';
          this.paymentInput = '';
          this.filter = this.viewInvSrv.getStatusId(this.filter) as string;
          break;
        case 'Payment':
          this.createdDateInput = '';
          this.statusInput = '';
          this.filter = this.viewInvSrv.getPaymetId(this.filter) as string;
          break;
        default:{
          this.paymentInput = '';
          this.statusInput = '';
        }
      }
      this.getInvoices(this.filter,this.filterBy,this.sortOrder,this.page);
    }
  }

  onClear(){
    this.createdDateInput = '';
    this.paymentInput = '';
    this.statusInput = '';
    this.filter = '';
    this.filterBy = '';
    this.sortOrder = '';
    this.page = 0;
    this.getInvoices(this.filter,this.filterBy,this.sortOrder,this.page);
  }

  onRowEditInit(invoice: Invoice) {
      this.clonedInvoice[invoice.id] = {...invoice};
  }

  onRowEditSave(invoice: Invoice) {
      this.api.update(invoice).subscribe(()=>{
        this.msgServ.add({severity:'success', summary:'Service Message', detail:'Updated success'});
      },(err:any)=>{
        this.msgServ.add({severity:'error', summary:'Service Message', detail:'Update failed'});
        console.log(err);
      })
  }

  onRowEditCancel(invoice: Invoice, index: number) {
      this.invoices[index] = this.clonedInvoice[invoice.id];
      delete this.clonedInvoice[invoice.id];
  }

  openNew() {
      this.invoiceToSave = new Invoice();
      this.invoiceToSave.payment = PaymentMethods.DebitCard;
      this.invoiceToSave.status = Statuses.New;
      this.invoiceToSave.creationDate = new Date();
      this.showDialog = true;
  }

  hideDialog() {
    this.showDialog = false;
    this.submitted = false;
  }
  saveInvoice(){
    this.submitted = true;
    if(this.invoiceToSave.amount >0 && this.invoiceToSave.number > 0){
      this.api.insert(this.invoiceToSave).subscribe(()=>{
        this.showDialog = false;
        this.msgServ.add({severity:'success', summary:'Service Message', detail:'Saved success'});
        this.getInvoices(this.filter,this.filterBy,this.sortOrder,this.page);
      },(err:any)=>{
        this.msgServ.add({severity:'error', summary:'Service Message', detail:'Saved failed'});
        console.log(err);
      })
    }
  }
}
