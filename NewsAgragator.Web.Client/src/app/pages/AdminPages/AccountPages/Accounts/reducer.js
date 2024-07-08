import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "ACCOUNT_LOADALL_SUCCESS":
            return {
                ...state,
                accounts:
                {
                    loading: false,
                    
                    value: action.data.map(tpr => ({
                        ...Object.fromEntries(Object.entries(tpr).map(x => ([
                            x[0],
                            {
                                value: x[1],
                            },
                        ]))),
                        accountStatus: {
                             ...Object.fromEntries(Object.entries(tpr.accountStatus).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        role: {
                            ...Object.fromEntries(Object.entries(tpr.role).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                    })),
                },
            }

        case "ACCOUNT_REMOVE_START":
            return {
                ...state,
                accounts:
                {
                    loading: true,
                    value:
                        [
                            {
                                id: {
                                    value: '',
                                },
                                userName: {
                                    value: '',
                                },
                                fio: {
                                    value: '',
                                },
                                email: {
                                    value: '',
                                },
                                phone: {
                                    value: '',
                                },
                                desiredNewsRating: {
                                    value: '',
                                },
                                login: {
                                    value: '',
                                },
                                password: {
                                    value: '',
                                },
                                accountStatus: {
                                    id: '',
                                    text: '',
                                },
                                role: {
                                    id: '',
                                    text: '',
                                },
                            },
                        ],
                }
            }

        

        case "ACCOUNT_LOADALL_START":
            return {
                ...state,
                accounts:
                {
                    loading: true,
                    value:
                        [
                            {
                                id: {
                                    value: '',
                                },
                                userName: {
                                    value: '',
                                },
                                fio: {
                                    value: '',
                                },
                                email: {
                                    value: '',
                                },
                                phone: {
                                    value: '',
                                },
                                desiredNewsRating: {
                                    value: '',
                                },
                                login: {
                                    value: '',
                                },
                                password: {
                                    value: '',
                                },
                                accountStatus: {
                                    id:'',
                                    text: '',
                                },
                                role: {
                                    id: '',
                                    text: '',
                                },
                            },
                        ],
                }
            }

        default:
            return state
    }
}