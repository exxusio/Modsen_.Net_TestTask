export class KeyByValueHelper {
    static getKeyByValue(object: any, value: any): string {
        if (object && value) {
            for (const key of Object.keys(object)) {
                if (object[key] === value) {
                    return key;
                }
            }
        }
        return 'Key not found';
    }

    static getKeyByNestedValue(
        object: any,
        value: any,
        nestedValue?: any
    ): string {
        if (nestedValue !== undefined) {
            for (const key of Object.keys(object)) {
                if (object[key] === value) {
                    for (const nestedKey of Object.keys(value)) {
                        if (value[nestedKey] === nestedValue) {
                            return `${key}.${nestedKey}`;
                        }
                    }
                }
            }
        } else {
            for (const key of Object.keys(object)) {
                if (object[key] === value) {
                    return key;
                }
            }
        }

        return 'Key not found';
    }
}
