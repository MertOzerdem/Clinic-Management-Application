export interface ITransaction {

    patientId : number;
    method : string;
    Amount : number;
}

export class Transaction implements ITransaction{

    constructor(
        public patientId: number,
        public method: string,
        public Amount: number ){}
}
