export interface IServiceTypes {
    serviceTypeId : number;
    fee : number;
    type : string;
}

export class ServiceType implements IServiceTypes{

    constructor(public serviceTypeId: number,
        public fee: number,
        public type: string){}
}