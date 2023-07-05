import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthorizationService } from "./authorization.service";


@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {

    constructor(
        private authService: AuthorizationService
    ) { }


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(
            req.clone({
                headers: req.headers.append(
                    'Authorization',
                    'Bearer ' + this.authService.userToken
                )
            })
        );
    }
}