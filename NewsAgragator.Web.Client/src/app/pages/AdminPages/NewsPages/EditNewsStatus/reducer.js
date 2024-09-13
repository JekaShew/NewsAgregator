import initialState from "../../../../../initialState";



export default (state = initialState.editNewsStatus, action) => {
    switch (action.type) {

        case "NEWSSTATUS_SELECT":
            return {
                ...state,
                // editNewsStatus:
                // {
                //     ...state.editNewsStatus,
                    [action.name]:
                    {
                        value: action.val,
                    },
                // }
            }

        case "NEWSSTATUS_LOAD_START":
            return {
                ...state,
                // editNewsStatus:
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

        case "NEWSSTATUS_LOAD_SUCCESS":
            return {
                ...state,
                // editNewsStatus:
                // {
                //     ...state.editNewsStatus,
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