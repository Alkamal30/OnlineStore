import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IProductModel } from '../../models/product.model';
import { AuthorizationService } from '../../services/authorization.service';
import { IOrderItemModel } from 'src/app/models/order-item.model';
import { BreakpointObserver } from '@angular/cdk/layout';


@Component({
    selector: 'cart-card',
    templateUrl: './cart-card.component.html',
    styleUrls: ['./cart-card.component.css'],
    providers: [ 
        AuthorizationService
    ]
})
export class CartCardComponent {
    
    constructor(
        public breakpointObserver: BreakpointObserver,
        public authService: AuthorizationService
    ) { }


    @Input() orderItem: IOrderItemModel;
    
    @Output() onCountChangedEvent: EventEmitter<IOrderItemModel> = new EventEmitter<IOrderItemModel>();
    @Output() removeEvent: EventEmitter<IOrderItemModel> = new EventEmitter<IOrderItemModel>();


    ngOnInit() {
        
    }


    onCountChanged() {
        this.onCountChangedEvent.emit(this.orderItem);
    }

    remove() {

        this.removeEvent.emit(this.orderItem);
    }
}
