export interface IProduct {
    Fee : number;
    Type : string
}

export class Product implements IProduct{

    constructor(public Fee: number,
                public Type : string){}
}
