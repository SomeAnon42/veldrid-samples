#version 330 core

uniform ProjView
{
    mat4 View;
    mat4 Proj;
};

in vec3 Position;
in vec3 Normal;
in vec2 TexCoord;

out vec3 fsin_Position_WorldSpace;
out vec3 fsin_Normal;
out vec2 fsin_TexCoord;

void main()
{
    fsin_Position_WorldSpace = Position;
    vec4 pos = vec4(Position, 1);
    gl_Position = Proj * View * pos;
    gl_Position.z = gl_Position.z * 2.0 - gl_Position.w;
    fsin_Normal = Normal;
    fsin_TexCoord = TexCoord * vec2(10, 6);
}
