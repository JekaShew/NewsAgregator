import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NEWSSTATUS_SELECT":
            return {
                ...state,
                editNewsStatus:
                {
                    ...state.editNewsStatus,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case "NEWSSTATUS_LOAD_SUCCESS":
            return {
                ...state,
                editNewsStatus:
                {
                    ...state.editNewsStatus,
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