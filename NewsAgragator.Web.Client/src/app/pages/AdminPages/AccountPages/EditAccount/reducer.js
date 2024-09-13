import initialState from "../../../../../initialState";


export default (state = initialState.editAccount, action) => {
    switch (action.type) {

        case "ACCOUNT_SELECT":
            return {
                ...state,
                // editAccount:
                // {
                //     ...state.editAccount,
                [action.name]:
                {
                    value: action.val,
                },
                // }
            }

        case "ACCOUNT_SELECT_PARAMETER":
            return {
                ...state,
                // editAccount:
                // {
                //     ...state.editAccount,
                [action.name]:
                {
                    id: action.val.value,
                    text: action.val.text,
                },
                // }
            }

        case "ACCOUNT_LOAD_PARAMETERS_START":
            return {
                ...state,
                // editAccount:
                // {
                loadingParameters: true,
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
                confirmationPassword: {
                    value: '',
                },
                secretWord: {
                    value: '',
                },

                accountStatus: {
                    value: {
                        id: '',
                        text: '',
                    },
                },

                role: {
                    value: {
                        id: '',
                        text: '',
                    },
                },

                accountStatuses: {
                    value: {
                        id: '',
                        text: '',
                    },
                    options: [
                        {
                            id: '',
                            text: ''
                        },
                    ],
                },
                roles: {
                    value: {
                        id: '',
                        text: '',
                    },
                    options: [
                        {
                            id: '',
                            text: ''
                        },
                    ],
                },

                // }
            }

        case 'ACCOUNT_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                // editAccount:
                // {
                //     ...state.editAccount,
                loadingParameters: false,
                ...Object.fromEntries(Object.entries(action.data).map(x => ([
                    x[0],
                    {

                        value: x[1],
                    }
                ])))
                // }

            }

        case "ACCOUNT_LOAD_START":
            return {
                ...state,
                // ...state.editAccount,
                // editAccount:
                // {
                loadingData: true,

                // }
            }

        case "ACCOUNT_LOAD_SUCCESS":
            return {
                ...state,
                // editAccount:
                // {
                ...state.accountStatuses,
                ...state.roles,
                loadingData: false,

                ...Object.fromEntries(Object.entries(action.data).map(x => ([
                    x[0],
                    {
                        value: x[1],

                    }
                ]))),
                confirmationPassword:
                {
                    value: action.data.password,
                }
                // }
            }



        default:
            return state
    }
}