import { Component, OnDestroy, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { IOrderModel } from "src/app/models/order.model";
import { AuthorizationService } from "src/app/services/authorization.service";
import { OrdersService } from "src/app/services/orders.service";


@Component({
    selector: 'app-my-order',
    templateUrl: './my-orders.component.html',
    styleUrls: ['./my-orders.component.css'],
    providers: [
        AuthorizationService,
        OrdersService
    ]
})
export class MyOrdersComponent implements OnInit {
    orders$: Observable<IOrderModel[]>
    expandSet: Set<number> = new Set();

    constructor(
        public authService: AuthorizationService,
        private ordersService: OrdersService
    ) { }


    ngOnInit(): void {
        this.orders$ = this.ordersService.getAllOwn();
    }

    onExpandChange(id: number, checked: boolean): void {
        if(checked)
            this.expandSet.add(id);
        else this.expandSet.delete(id);
    }


    countUnits(order: IOrderModel): number {
        let counter: number = 0;

        order.orderItems.forEach(
            x => counter += x.count
        );

        return counter;
    }

    calculateTotalCost(order: IOrderModel): number {
        let totalCost: number = 0;

        order.orderItems.forEach(
            x => totalCost += x.count * x.product.price
        );

        return totalCost;
    }
}