import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})

export class BackendService {
    _baseUrl:string;
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl=baseUrl;
   }

   AddProduct(obj)
   {
      return this.httpClient.post(this._baseUrl + 'api/Products/AddProduct',obj)
   }
   RemoveProduct(invid)
   {
    return this.httpClient.get(this._baseUrl + 'api/Products/RemoveProduct?invid='+invid) 
   }
   GetData()
   {
    return this.httpClient.get(this._baseUrl + 'api/Products/GetData')   
   }
}