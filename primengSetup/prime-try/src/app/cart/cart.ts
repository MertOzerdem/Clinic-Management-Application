export interface ICart {
    patientId : number;
    paymentId: number;
    method : string;
    amount : number;
}

export class Cart implements ICart{

    constructor(
        public patientId: number,
        public paymentId: number,
        public method: string,
        public amount: number ){}
}