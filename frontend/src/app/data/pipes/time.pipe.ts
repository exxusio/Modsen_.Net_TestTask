import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'time',
    standalone: true,
})
export class TimePipe implements PipeTransform {
    transform(value: string, format: string = 'HH:mm'): string {
        if (!value) return '';

        const timeParts = value.split(':');

        if (timeParts.length < 2) return value;

        const hours = timeParts[0];
        const minutes = timeParts[1];
        const seconds =
            timeParts.length > 2 ? timeParts[2].split('.')[0] : '00';
        const milliseconds =
            timeParts.length > 2 ? timeParts[2].split('.')[1] || '000' : '000';

        let formattedTime = format;
        formattedTime = formattedTime.replace('HH', hours);
        formattedTime = formattedTime.replace('mm', minutes);
        formattedTime = formattedTime.replace('ss', seconds);
        formattedTime = formattedTime.replace('sss', milliseconds);

        return formattedTime;
    }
}
