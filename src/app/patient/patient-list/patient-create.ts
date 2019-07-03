export interface IPatientCreate {
    Name : string;
    Surname : string;
}

export class PatientCreate implements IPatientCreate{

    constructor(
        public Name: string,
        public Surname: string){

    }
    
}