import initialState from "../../../../../initialState";
import { loadParameters } from "./actions";


export default (state = initialState.editAccount, action) => {
    switch (action.type) {

        case "ACCOUNT_SELECT":
            return {
                ...state,
                [action.name]:
                {
                    value: action.val,
                },
            }

        case "ACCOUNT_SELECT_PARAMETER":
            return {
                ...state,
                [action.name]:
                {
                    id: action.val.value,
                    text: action.val.text,
                },
            }

        case "ACCOUNT_LOAD_PARAMETERS_START":
            return {
                ...state,
                loadingParameters: true,
                loadingData:false,
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

                loadingParameters: false,
                loadingData:true,
                ...Object.fromEntries(Object.entries(action.data).map(x => ([
                    x[0],
                    {

                        value: x[1],
                    }
                ])))

            }

        case "ACCOUNT_LOAD_START":
            return {
                ...state,
                // ...state.editAccount,
                // editAccount:
                // {
                loadingData: true,
                loadingParameters: false,

                // }
            }

        case "ACCOUNT_LOAD_SUCCESS":
            return {
                ...state,
                ...state.accountStatuses,
                ...state.roles,
                loadingData: false,
                loadingParameters: false,

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

            }



        default:
            return state
    }
}