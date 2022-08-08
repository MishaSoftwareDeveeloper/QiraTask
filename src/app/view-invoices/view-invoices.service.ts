import { Injectable } from '@angular/core';
import { PaymentMethods } from '../Models/payment-methods.model';
import { Statuses } from '../Models/statuses.model';

@Injectable({
  providedIn: 'root'
})
export class ViewInvoicesService {

  constructor() { }

  getStatusName(status:number){
    console.log(status)
    switch(status){
      case Statuses.New:return 'New'
      case Statuses.Canceled:return 'Canceled'
      case Statuses.Paid:return 'Paid'
      default : return ''
    }
  }

  getStatusId(statusName:string){
    if('new'.includes(statusName.toLowerCase())){
      return Statuses.New;
    }
    if('canceled'.includes(statusName.toLowerCase())){
      return Statuses.Canceled;
    }
    if('paid'.includes(statusName.toLowerCase())){
      return Statuses.Paid;
    }
    return '';
  }


  getPaymentName(payment:number){
    switch(payment){
      case PaymentMethods.CreditCard:return 'Credit Card'
      case PaymentMethods.DebitCard:return 'Debit Card'
      case PaymentMethods.ElectronicCheck:return 'Electronic Check'
      default : return ''
    }
  }

  getPaymetId(paymentName:string){
    if('credit card'.includes(paymentName.toLowerCase())){
      return PaymentMethods.CreditCard;
    }
    if('debit card'.includes(paymentName.toLowerCase())){
      return PaymentMethods.DebitCard;
    }
    if('electronic check'.includes(paymentName.toLowerCase())){
      return PaymentMethods.ElectronicCheck;
    }
    return '';
  }
}
