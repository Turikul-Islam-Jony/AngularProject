import { Injectable } from '@angular/core';
import { from } from 'rxjs';
import{HttpClient}from '@angular/common/http'
import { PaymentDetail } from './payment-detail.model';

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {
  formData:PaymentDetail=new PaymentDetail();
  list:PaymentDetail[];

  constructor(private http:HttpClient) { 
    this.list=[];
  }
 
  readonly baseUrl='http://localhost:53878/api/PaymentDetail';
  postPaymentDetail(){
    return this.http.post(this.baseUrl,this.formData);
  }
  putPaymentDetail()
  {
    return this.http.put(`${this.baseUrl}/${this.formData.paymentDetailId}`,this.formData);
  }
  deletePaymentDetail(id:number){
return this.http.delete(`${this.baseUrl}/${id}`);
  }
  refreshList(){
    this.http.get(this.baseUrl).toPromise().then(res=>{
       this.list=res as PaymentDetail[];
    })
  }
}
