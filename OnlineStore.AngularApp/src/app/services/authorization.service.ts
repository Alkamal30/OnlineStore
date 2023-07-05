import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IUserAuthorizationModel } from "../models/user-auth.model";
import { CookieService } from "ngx-cookie-service";
import { Router } from "@angular/router";
import { IUserAuthorizationResponseModel } from "../models/user-auth-response.model";
import { IProductModel } from "../models/product.model";
import { IOrderItemModel } from "../models/order-item.model";


@Injectable()
export class AuthorizationService {

    constructor(
        private httpClient: HttpClient,
        private cookieService: CookieService,
        private router: Router
    ) { }

    private url: string = "https://localhost:7201/api/Authorize/";
    
    public get userLogin(): string {
        if(!this.cookieService.check('user-login'))
            return "";

        return this.cookieService.get('user-login');
    }

    public get userToken(): string {
        if(!this.cookieService.check('user-token'))
            return "";

        return this.cookieService.get('user-token');
    }

    public get userRole(): string {
        if(!this.cookieService.check('user-role'))
            return "";

        return this.cookieService.get('user-role');
    }

    public get isAdmin(): boolean {
        if(this.userRole == undefined || this.userRole != 'admin')
            return false;

        return true;
    }


    public get userShoppingCart(): IOrderItemModel[] {
        if(!this.cookieService.check('user-shopping-cart'))
            return [];

        return JSON.parse(this.cookieService.get('user-shopping-cart'));
    }

    public set userShoppingCart(cartItems: IOrderItemModel[]) {
        this.cookieService.set(
            'user-shopping-cart', 
            JSON.stringify(cartItems)    
        )
    }



    tryLogIn(authModel: IUserAuthorizationModel) {
        let response = this.httpClient
            .post(this.url, authModel, { observe: 'response'})
            .subscribe(response => {
                if(!response.ok)
                    return;
                
                if(response.body == null)
                    return;

                if(response.body == 'Incorrect Login or Password!')
                    return;

                let responseModel = response.body as IUserAuthorizationResponseModel;

                this.cookieService.set('user-login', authModel.login);
                this.cookieService.set('user-token', responseModel.jwtToken);
                this.cookieService.set('user-role', (responseModel.role == 0) ? 'user' : 'admin');
            });
    }

    logOut() {
        this.cookieService.deleteAll();
    }

    unauthorizedRedirect() {
        this.router.navigate(['/home']);
    }
}