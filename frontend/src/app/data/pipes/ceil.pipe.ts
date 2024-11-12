import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'ceil',
    standalone: true,
})
export class CeilPipe implements PipeTransform {
    transform(value: number): number {
        if (value == null) return 0;
        return Math.ceil(value);
    }
}
