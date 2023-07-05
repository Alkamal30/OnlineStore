import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../services/authorization.service';
import { IOrderModel } from 'src/app/models/order.model';
import { Observable } from 'rxjs';
import { OrdersService } from 'src/app/services/orders.service';


@Component({
    selector: 'orders',
    templateUrl: './orders.component.html',
    styleUrls: ['./orders.component.css'],
    providers: [ 
        AuthorizationService,
        OrdersService
    ]
})
export class OrdersComponent implements OnInit {
    orders$: Observable<IOrderModel[]>;
    expandSet: Set<number> = new Set();


    constructor(
        public authService: AuthorizationService,
        private ordersService: OrdersService
    ) { }
    
    
    ngOnInit(): void {
        this.orders$ = this.ordersService.getAll();
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
