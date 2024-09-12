import initialState from "../../../../../initialState";



export default (state = initialState.editAccountStatus, action) => {
    switch (action.type) {

        case "ACCOUNTSTATUS_SELECT":
            return {
                ...state,
                // editAccountStatus:
                // {
                //     ...state.editAccountStatus,
                [action.name]:
                {
                    value: action.val,
                },
                // }
            }



        case "ACCOUNTSTATUS_LOAD_START":
            return {
                ...state,
                // editAccountStatus:
                // {
                loading: true,
                id: {
                    value: '',
                },
                title: {
                    value: '',
                },
                description: {
                    value: '',
                }
                // }
            }

        case "ACCOUNTSTATUS_LOAD_SUCCESS":
            return {
                ...state,
                // editAccountStatus:
                // {
                //     ...state.editAccountStatus,
                loading: false,
                ...Object.fromEntries(Object.entries(action.data).map(x => ([
                    x[0],
                    {
                        value: x[1],
                    }
                ]))),
                // }
            }

        default:
            return state
    }
}