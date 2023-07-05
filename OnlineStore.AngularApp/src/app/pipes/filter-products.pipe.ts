import { Pipe, PipeTransform } from "@angular/core";
import { IProductModel } from "../models/product.model";

@Pipe({
    name: 'filterProducts'
})
export class FilterProductsPipe implements PipeTransform {

    transform(value: IProductModel[], filter: string, options: Array<{label: string, value: string, checked?: boolean}>): IProductModel[] {
        return value.filter(x => 
            (options[0].checked && x.title.toLowerCase().includes(filter.toLowerCase()))
            || (options[1].checked && x.description.toLowerCase().includes(filter.toLowerCase()))
        ); 
    }
}