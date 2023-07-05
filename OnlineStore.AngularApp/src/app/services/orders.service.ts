import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IProductModel } from "../models/product.model";
import { CookieService } from "ngx-cookie-service";
import { IOrderModel } from "../models/order.model";
import { Observable } from "rxjs";
import { IOrderPostModel } from "../models/order-post.model";
import { IOrderCheckoutModel } from "../models/order-checkout.model";


@Injectable()
export class OrdersService {

    constructor(
        private httpClient: HttpClient, 
        private cookieService: CookieService 
    ) { }


    private url = "https://localhost:7201/api/Order/";


    getAll(): Observable<IOrderModel[]> {
        return this.httpClient.get<IOrderModel[]>(this.url);
    }

    getAllOwn(): Observable<IOrderModel[]> {
        return this.httpClient.get<IOrderModel[]>(this.url + 'own');
    }

    getById(id: number): Observable<IOrderModel> {
        return this.httpClient.get<IOrderModel>(this.url + id);
    }

    
    add(order: IOrderPostModel) {
        return this.httpClient.post(this.url, order);
    }

    checkout(order: IOrderCheckoutModel) {
        console.log('checkout')
        return this.httpClient.post(
            this.url + 'checkout',
            order
        ).subscribe(x => console.log(x));
    }
}