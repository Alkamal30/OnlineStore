import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IProductModel } from 'src/app/models/product.model';
import { FallbackImage } from 'src/app/utilities/fallback-image';

@Component({
    selector: 'app-editable-product',
    templateUrl: './editable-product.component.html',
    styleUrls: ['./editable-product.component.css']
})
export class EditableProductComponent {

    @Input() product: IProductModel;

    @Output() startEditEvent: EventEmitter<IProductModel> = new EventEmitter();
    @Output() removeEvent: EventEmitter<IProductModel> = new EventEmitter();
    

    constructor() { }


    startEdit() {
        this.startEditEvent?.emit(this.product);
    }

    remove() {
        this.removeEvent?.emit(this.product);
    }

    getFallbackImage(): string {
        return FallbackImage.Base64;
    }
}
