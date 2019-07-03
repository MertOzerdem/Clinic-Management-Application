export interface INewPatient {
    Name : string;
    Surname : string;
    patientId : number;
}

export class NewPatient implements INewPatient{
    patientId: number;

    constructor(
        public Name: string,
        public Surname: string){

    }
    
}