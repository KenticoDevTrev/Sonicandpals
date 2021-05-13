export class AjaxHelper {

constructor() {
    
}

async postRequest<T>(Url: string, Data?: any): Promise<T> {
    const response = await fetch(Url, {
        method: "POST",
        mode: 'cors',
        cache: "no-cache",
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        body: (Data ? JSON.stringify(Data) : "")
    });
    return await response.json();
}

async getRequest<T>(Url: string, Data?: any): Promise<T> {
    const response = await fetch(Url, {
        method: "GET",
        mode: 'cors',
        cache: "no-cache",
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json'
        },
        body: (Data ? JSON.stringify(Data) : "")
    });
    return await response.json();
}
}