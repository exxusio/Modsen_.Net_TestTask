import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export class CustomValidators {
    static equalTo(controlNameToCompare: string): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            if (!control.parent) {
                return null;
            }

            const controlToCompare = control.parent.get(controlNameToCompare);
            if (!controlToCompare) {
                return null;
            }

            const isEqual = control.value === controlToCompare.value;
            if (isEqual) {
                return null;
            }

            return { notEqual: true };
        };
    }

    static dateValidator(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const dateRegex =
                /^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.\d{4}$/;
            const isValidFormat = dateRegex.test(control.value);

            if (!isValidFormat) {
                return {
                    invalidDateFormat: true,
                };
            }

            const [day, month, year] = control.value.split('.').map(Number);
            const inputDate = new Date(year, month - 1, day);
            const currentDate = new Date();

            if (inputDate > currentDate) {
                return { dateTooFuture: true };
            }

            if (
                inputDate.getDate() !== day ||
                inputDate.getMonth() + 1 !== month ||
                inputDate.getFullYear() !== year
            ) {
                return { invalidDate: true };
            }

            return null;
        };
    }

    static futureDateValidator(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const currentDate = new Date();
            const inputDate = new Date(control.value);

            if (
                control.value &&
                inputDate.setHours(0, 0, 0, 0) <=
                    currentDate.setHours(0, 0, 0, 0)
            ) {
                return { pastDate: true };
            }

            return null;
        };
    }
}
