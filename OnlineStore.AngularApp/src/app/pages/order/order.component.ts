import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { IOrderModel } from 'src/app/models/order.model';
import { AuthorizationService } from 'src/app/services/authorization.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
    
    @Input() model: IOrderModel;

    @Output() acceptEvent: EventEmitter<OrderComponent>;
    @Output() rejectEvent: EventEmitter<OrderComponent>;


    constructor(
        public authService: AuthorizationService
    ) { }
    

    accept() {
        this.acceptEvent?.emit(this);
    }

    reject() {
        this.rejectEvent?.emit(this);
    }

    countTotalCost(): number {
        let totalCost = 0;

        this.model.orderItems.forEach(x => {
            totalCost += x.product.price * x.count;
        });

        return totalCost;
    }
}
