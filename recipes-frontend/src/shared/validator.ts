import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function hasUpperCase(): ValidatorFn {
    type NewType = ValidationErrors;

    return (control: AbstractControl): NewType | null => {

        const value = control.value;

        if (!value) {
            return null;
        }
        const hasUpperCase = /[A-Z]+/.test(value);

        return !hasUpperCase ? { UpperCaseStrength: true } : null;
    }
}

export function hasLowerCase(): ValidatorFn {
    type NewType = ValidationErrors;

    return (control: AbstractControl): NewType | null => {

        const value = control.value;

        if (!value) {
            return null;
        }
        const hasLowerCase = /[a-z]+/.test(value);

        return !hasLowerCase ? { LowerCaseStrength: true } : null;
    }
}

export function hasNumeric(): ValidatorFn {
    type NewType = ValidationErrors;

    return (control: AbstractControl): NewType | null => {

        const value = control.value;

        if (!value) {
            return null;
        }
        const hasNumeric = /[0-9]+/.test(value);

        return !hasNumeric ? { NumericStrength: true } : null;
    }
}

export function matchValidator(
    matchTo: string,
    reverse?: boolean
): ValidatorFn {
    return (control: AbstractControl):
        ValidationErrors | null => {
        if (control.parent && reverse) {
            const c = (control.parent?.controls as any)[matchTo] as AbstractControl;
            if (c) {
                c.updateValueAndValidity();
            }
            return null;
        }
        return !!control.parent &&
            !!control.parent.value &&
            control.value ===
            (control.parent?.controls as any)[matchTo].value
            ? null
            : { matching: true };
    };
}

export function isphone(): ValidatorFn {
    type NewType = ValidationErrors;

    return (control: AbstractControl): NewType | null => {

        const value = control.value;

        if (!value) {
            return null;
        }
        const hasNumeric = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/.test(value);

        return !hasNumeric ? { IsPhone: true } : null;
    }
}