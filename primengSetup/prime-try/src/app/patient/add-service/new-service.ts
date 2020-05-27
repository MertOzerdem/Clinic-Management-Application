import { Type } from "./type";

export interface INewService 
{
    discount : number;
    patientId : number;
    provider : string;
    providedServices : Type[];
}

export class Service implements INewService{

    constructor(
        public discount : number,
        public patientId : number,
        public provider : string,
        public providedServices : Type[]
    ){}
}
