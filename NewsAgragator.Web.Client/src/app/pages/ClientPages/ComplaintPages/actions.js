export const sendComplaint = (newComplaint) => ({
    type: 'COMPLAINT_SEND',
    remote: {
        url: '/api/complaint/sendcomplaint',
        type: 'post',
        body: newComplaint,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'COMPLAINT_SELECT',
    name: name,
    val: value,
})

export const selectParameter = (name, value, text) => ({
    type: 'COMPLAINT_SELECT',
    name: name,
    val: { id: value, text: text },
})


export const loadParameters = () => ({
    type: 'COMPLAINT_LOAD_PARAMETERS',
    remote: {
        url: '/api/complaint/getparameters',
        type: 'get',

    }

})

export const clearState = () => ({
    type: 'COMPLAINT_LOAD_PARAMETERS_SUCCESS',

    data:
    {
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        text: {
            value: '',
        },
    }
})

