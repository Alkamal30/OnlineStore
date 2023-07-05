import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IProductModel } from "../models/product.model";
import { CookieService } from "ngx-cookie-service";


@Injectable()
export class ProductsCrudService {

    constructor(
        private httpClient: HttpClient, 
        private cookieService: CookieService 
    ) { }


    private url = "https://localhost:7201/api/Product/";

    
    getProducts() {
        return this.httpClient.get<IProductModel[]>(this.url);
    }

    getProduct(id: number) {
        return this.httpClient.get<IProductModel>(this.url + id);
    }

    addProduct(product: IProductModel) {
        return this.httpClient.post(this.url, product);
    }

    updateProduct(product: IProductModel) {
        return this.httpClient.put(this.url, product);
    }

    removeProduct(productId: number) {
        return this.httpClient.delete(this.url + productId);
    }
}