import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { NzNotificationService } from "ng-zorro-antd/notification";
import { Observable, tap } from "rxjs";


@Injectable()
export class ErrorsInterceptor implements HttpInterceptor {
    
    constructor(
        private notificationService: NzNotificationService
    ) { }


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req.clone()).pipe(
            tap(
                (event) => { },
                (err) => {
                    if(err instanceof HttpErrorResponse) {
                        console.log(err.message);
                        this.notificationService.error(
                            'HTTP ' + err.status + " Status Code",
                            err.message,
                            { nzPlacement: 'top' }
                        )
                    }
                }
            )
        );
    }
}