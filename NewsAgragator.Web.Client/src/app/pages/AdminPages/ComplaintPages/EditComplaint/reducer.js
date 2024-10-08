import initialState from "../../../../../initialState";



export default (state = initialState.editComplaint, action) => {
    switch (action.type) {

        case "COMPLAINT_SELECT":
            return {
                ...state,
                // editComplaint:
                // {
                //     ...state.editComplaint,
                    [action.name]:
                    {
                        value: action.val,
                    },
                // }
            }

        case "COMPLAINT_SELECT_PARAMETER":
            return {
                ...state,
                // editComplaint:
                // {
                //     ...state.editComplaint,
                    [action.name]:
                    {

                        id: action.val.value,
                        text: action.val.text,

                    },
                // }
            }

        case "COMPLAINT_LOAD_PARAMETERS_START":
            return {
                ...state,
                // editComplaint:
                // {
                    loadingParameters: true,
                    loadingData:false,
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },
                    comment: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    comments: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    news: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    newses: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    complaintStatus: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    complaintStatuses: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    complaintType: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    complaintTypes: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    user: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    administrator: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                    accounts: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },
                // }
            }

        case 'COMPLAINT_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                // editComplaint:
                // {
                //     ...state.editComplaint,                   
                loadingParameters: false,
                loadingData:true,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {

                            value: x[1],
                        },     
                    ])))
                   
                // }

            }

        case "COMPLAINT_LOAD_START":
            return {
                ...state,
                    loadingData: true,
                    loadingParameters: false,
            }

        case "COMPLAINT_LOAD_SUCCESS":
            return {
                ...state,
                // editComplaint:
                // {
                //     ...state.editComplaint,
                    ...state.comments,
                    ...state.newses,
                    ...state.complaintStatuses,
                    ...state.complaintTypes,
                    ...state.accounts,
                    loadingData: false,
                    loadingParameters: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            // }

        default:
            return state
    }
}