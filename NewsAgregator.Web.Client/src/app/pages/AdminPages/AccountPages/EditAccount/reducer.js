import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "ACCOUNT_SELECT":
            return {
                ...state,
                editAccount:
                {
                    ...state.editAccount,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case 'ACCOUNT_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                editAccount:
                {
                    ...state.editAccount,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: state.editAccount[x[0]].value,
                            options: x[1],
                        }
                    ])))
                }

            }

        case "ACCOUNT_LOAD_SUCCESS":
            return {
                ...state,
                editAccount:
                {
                    ...state.editAccount,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            }

        default:
            return state
    }
}