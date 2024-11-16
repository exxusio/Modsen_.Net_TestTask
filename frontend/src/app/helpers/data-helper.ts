export class DataHelper {
    static formatDateToDisplay(date: string): string {
        const parsedDate = new Date(date);
        const day = String(parsedDate.getDate()).padStart(2, '0');
        const month = String(parsedDate.getMonth() + 1).padStart(2, '0');
        const year = parsedDate.getFullYear();

        return `${day}.${month}.${year}`;
    }

    static formatDateForServer(date: string): string {
        const [day, month, year] = date.split('.');
        return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
    }
}
