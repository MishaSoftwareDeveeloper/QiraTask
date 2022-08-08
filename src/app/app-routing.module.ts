import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ViewInvoicesComponent } from './view-invoices/view-invoices.component';

const routes: Routes = [
  { path: '*', component: ViewInvoicesComponent },
  { path: '**', component: ViewInvoicesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
