import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  //Get,insert, update and delete on server side
  private httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
  };
    
  constructor(private http: HttpClient) { }
  get()
  {
    return this.http.get('api/Invoices');
  }
  getFiltered(filter:string,filterdBy:string,sortOrder:string,page:number){
    const endcodedValue = encodeURIComponent(filter);
    let options = {
      headers:this.httpOptions.headers,
      params : {
        paging: page,
        filter:filter,
        filterBy:filterdBy,
        sortOrder:sortOrder
      }
    }
    return this.http.get(`api/Invoices/Filter`,options);
  }
 
  insert(invoice:any)
  {
      return this.http.post("api/Invoices/", invoice, this.httpOptions);
  }

  update(invoice:any)
  {
      return this.http.put("api/Invoices/", invoice, this.httpOptions);
  }

}
