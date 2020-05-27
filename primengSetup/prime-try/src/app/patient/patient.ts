export interface IPatient {

    patientId : number;
    name : string;
    surname : string;
    image : string;
    balance : number;
}

export class Patient implements IPatient{

    constructor(
        public patientId: number,
        public name: string,
        public surname: string,
        public image: string,
        public balance: number){

    }
}